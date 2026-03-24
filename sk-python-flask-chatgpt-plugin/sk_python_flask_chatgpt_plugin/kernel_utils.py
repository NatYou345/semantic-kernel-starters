import os
import logging
from semantic_kernel import Kernel
import semantic_kernel.connectors.ai.open_ai as sk_oai
from semantic_kernel.functions import KernelArguments

from sk_python_flask_chatgpt_plugin.config import(
    AIService,
    headers_to_config,
    dotenv_to_config,
)


SKILLS_DIRECTORY = os.path.join("skills")


def create_kernel_for_request(request_headers, skill_name):
    """
    Creates a kernel for a request.
    :param request_headers: The request headers.
    :param skill_name: The skill/plugin name.
    :return: The kernel and plugin, or (None, error).
    """
    kernel = Kernel()
    logging.info(f"Creating kernel and importing skill {skill_name}")

    # Get the API configuration.
    try:
        api_config = headers_to_config(request_headers)
    except ValueError:
        logging.debug("No headers found. Using local .env file for configuration.")
        try:
            api_config = dotenv_to_config()
        except AssertionError:
            try:
                logging.debug("No Azure OpenAI found in .env file.")
                api_config = dotenv_to_config(use_azure_openai=False)
            except AssertionError:
                logging.debug("No valid .env file found.")
                return None, ("No valid headers found and no .env file found.", 400)

    try:
        if (
            api_config.serviceid == AIService.OPENAI.value
            or api_config.serviceid == AIService.OPENAI.name
        ):
            kernel.add_service(
                sk_oai.OpenAIChatCompletion(
                    ai_model_id=api_config.deployment_model_id,
                    api_key=api_config.key,
                    org_id=api_config.org_id,
                ),
            )
        elif (
            api_config.serviceid == AIService.AZURE_OPENAI.value
            or api_config.serviceid == AIService.AZURE_OPENAI.name
        ):
            kernel.add_service(
                sk_oai.AzureChatCompletion(
                    deployment_name=api_config.deployment_model_id,
                    api_key=api_config.key,
                    endpoint=api_config.endpoint,
                ),
            )
    except ValueError as e:
        logging.exception(f"Error creating completion service: {e}")
        return None, (f"Error creating completion service: {e}", 400)

    try:
        kernel.add_plugin(parent_directory=SKILLS_DIRECTORY, plugin_name=skill_name)
    except ValueError as e:
        logging.exception(f"Cannot import skill: {e}")
        return None, (f"Cannot import skill {skill_name}", 404)

    return kernel, None


def create_arguments_from_request(request) -> KernelArguments:
    """
    Creates kernel arguments from a JSON body.
    :param request: The Flask request.
    :return: The kernel arguments.
    """
    req_body = {}
    try:
        req_body = request.get_json()
    except ValueError:
        logging.warning("No JSON body provided in request.")

    return KernelArguments(**req_body)
