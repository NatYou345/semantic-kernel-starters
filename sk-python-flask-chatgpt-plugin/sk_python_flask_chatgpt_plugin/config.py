from dataclasses import dataclass

from enum import Enum

import os

DEFAULT_OPENAI_MODEL = "gpt-4o-mini"


class AIService(Enum):
    AZURE_OPENAI = "AZURE_OPENAI"
    OPENAI = "OPENAI"


class SKHttpHeaders(Enum):
    COMPLETION_MODEL = "x-ms-sk-completion-model"
    COMPLETION_ENDPOINT = "x-ms-sk-completion-endpoint"
    COMPLETION_SERVICE = "x-ms-sk-completion-backend"
    COMPLETION_KEY = "x-ms-sk-completion-key"


@dataclass
class AIServiceConfig:
    deployment_model_id: str
    endpoint: str
    key: str
    serviceid: str
    org_id: str = None


def headers_to_config(headers: dict) -> AIServiceConfig:
    if SKHttpHeaders.COMPLETION_MODEL.value in headers:
        return AIServiceConfig(
            deployment_model_id=headers[SKHttpHeaders.COMPLETION_MODEL.value],
            endpoint=headers[SKHttpHeaders.COMPLETION_ENDPOINT.value],
            key=headers[SKHttpHeaders.COMPLETION_KEY.value],
            serviceid=headers[SKHttpHeaders.COMPLETION_SERVICE.value],
        )
    raise ValueError("No valid headers found")


def dotenv_to_config(use_azure_openai=True):
    if use_azure_openai:
        deployment_model_id = os.environ.get("AZURE_OPENAI_DEPLOYMENT_NAME", "")
        api_key = os.environ.get("AZURE_OPENAI_API_KEY", "")
        endpoint = os.environ.get("AZURE_OPENAI_ENDPOINT", "")
        assert deployment_model_id and api_key and endpoint, "Azure OpenAI settings not found"
        return AIServiceConfig(
            deployment_model_id=deployment_model_id,
            endpoint=endpoint,
            key=api_key,
            serviceid=AIService.AZURE_OPENAI.value,
        )
    else:
        api_key = os.environ.get("OPENAI_API_KEY", "")
        org_id = os.environ.get("OPENAI_ORG_ID", "")
        assert api_key, "OpenAI settings not found"
        return AIServiceConfig(
            deployment_model_id=DEFAULT_OPENAI_MODEL,
            endpoint=None,
            key=api_key,
            serviceid=AIService.OPENAI.value,
            org_id=org_id,
        )
