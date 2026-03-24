import os
import azure.functions as func
import logging
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.open_ai import (
    AzureChatCompletion,
    OpenAIChatCompletion,
)
from semantic_kernel.functions import KernelArguments

useAzureOpenAI = True

app = func.FunctionApp(http_auth_level=func.AuthLevel.ANONYMOUS)

@app.route(route="skills/{skill_name}/functions/{function_name}")
async def ExecuteFunction(req: func.HttpRequest) -> func.HttpResponse:
    skill_name = req.route_params.get('skill_name')
    function_name = req.route_params.get('function_name')

    if not skill_name or not function_name:
        logging.error(f"Skill name: {skill_name} or function name: {function_name} not provided")
        return func.HttpResponse("Please pass skill_name and function_name on the URL", status_code=400)

    kernel = create_kernel()

    skills_directory = "skills"
    try:
        plugin = kernel.add_plugin(
            parent_directory=skills_directory, plugin_name=skill_name
        )
    except Exception as e:
        logging.exception(f"Skill {skill_name} not found")
        return func.HttpResponse(f"Skill {skill_name} not found", status_code=404)

    if function_name not in plugin:
        logging.error(f"Function {function_name} not found in skill {skill_name}")
        return func.HttpResponse(f"Function {function_name} not found in skill {skill_name}", status_code=404)
    sk_function = plugin[function_name]

    req_body = {}
    try:
        req_body = req.get_json()
    except ValueError:
        logging.warning("No JSON body provided in request.")

    arguments = KernelArguments(**req_body)
    result = await kernel.invoke(sk_function, arguments=arguments)

    logging.info(f"Result: {result}")

    return func.HttpResponse(str(result))


def create_kernel():
    kernel = Kernel()

    if useAzureOpenAI:
        kernel.add_service(
            AzureChatCompletion(
                deployment_name=os.environ.get("AZURE_OPENAI_DEPLOYMENT_NAME", ""),
                endpoint=os.environ.get("AZURE_OPENAI_ENDPOINT", ""),
                api_key=os.environ.get("AZURE_OPENAI_API_KEY", ""),
            )
        )
    else:
        kernel.add_service(
            OpenAIChatCompletion(
                ai_model_id="gpt-4o-mini",
                api_key=os.environ.get("OPENAI_API_KEY", ""),
                org_id=os.environ.get("OPENAI_ORG_ID", ""),
            )
        )

    return kernel
