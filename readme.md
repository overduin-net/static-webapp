[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://github.com/codespaces/new?skip_quickstart=true&hide_repo_select=true&ref=main&repo=549779176&machine=basicLinux32gb&devcontainer_path=.devcontainer%2Fdevcontainer.json&location=WestEurope)

# Starter template for Azure Static WebApp

## Description

This repository contains a template with frontend and backend with azure table and blob storage for storing data and files. Project contains:
 - Angular 19
 - Material
 - Function v4 dotnet 8 (isolated)

This project uses Azure Static WebApp in the simpelest and cheapest way. External documentation:

https://learn.microsoft.com/en-us/azure/static-web-apps/

https://angular.io/

https://material.angular.io/ (responsive styling)

https://azure.github.io/static-web-apps-cli/


## Usage

### Installing local
(for mac or linux use 'sudo')
```
npm install -g @angular/cli
npm install -g azure-functions-core-tools@4
npm install -g @azure/static-web-apps-cli
npm install -g nswag
```

### Install vscode extentions

 - Azurite (mock the Azure Table Storage)


### Prepare Function
 - change filename extension 
   - api/local.settings.json.example --> api/local.settings.json


## Run application

### *Step 1*
Start [Azurite Table Service] and [Azurite Blob Service]

### *Step 2*
Install npm packages in the "web"
```
cd web
npm install
```

### *Step 3*
Open a second terminal and start the application

```
swa start
```
Application is active at
http://localhost:4280/



### *When model changes update frontend with nswag*
Make sure application is running like in "Step 3"

```
cd web
nswag run /runtime:Net60
```


# TIPS
 - *Data is not being stored*
   - make sure the [Azurite Table Service] is enabled
