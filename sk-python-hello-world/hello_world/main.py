import os
import asyncio
from semantic_kernel import Kernel
from semantic_kernel.connectors.ai.open_ai import (
    AzureChatCompletion,
    OpenAIChatCompletion,
)
from semantic_kernel.functions import KernelArguments

useAzureOpenAI = False


async def main():
    kernel = Kernel()

    # Configure AI service used by the kernel. Load settings from environment variables.
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

    skills_directory = "skills"

    fun_plugin = kernel.add_plugin(
        parent_directory=skills_directory, plugin_name="FunSkill"
    )

    joke_function = fun_plugin["Joke"]

    arguments = KernelArguments(input="time travel to dinosaur age", style="standup comedy")
    result = await kernel.invoke(joke_function, arguments=arguments)

    print(result)


if __name__ == "__main__":
    asyncio.run(main())
