npm install -g @angular/cli --no-interactive
npm install -g azure-functions-core-tools@4
npm install -g @azure/static-web-apps-cli
npm install -g nswag

cd web
npm install

cat <<EOT > ../api/local.settings.json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AZURE_STORAGE_CONNECTION_STRING": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
  },
  "Host": {
    "CORS": "*"
  }
}
EOT
