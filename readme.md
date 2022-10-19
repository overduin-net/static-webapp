# Template

Develop Static WebApp (swa)
https://azure.github.io/static-web-apps-cli/

```
npm install -g @azure/static-web-apps-cli
```

# check if installation is correct
```
swa --version
```
# Should print out the version number

In the root of the project
```
swa init
```

check if it read the project OK

```
swa start
```

Use Nswag to sync model and api's from banckend to frontend
```
npm install nswag -g
```

In other terminal update the API's and Models
```
nswag run /runtime:Net60
```

# Tips
...