
## Prerequisites
Install Latest LTS Version of nodejs from https://nodejs.org/en/download/

Install extension NPM Task Runner into Visual Studio 2019 https://github.com/madskristensen/NpmTaskRunner

Please ensure that Visual studio 2019 is at version 16.5 or higher. The solution uses Typescript linting (https://github.com/microsoft/JSTSdocs/blob/master/articles/editor/linting.md) and this is only supported after this version.

## Download npm version 
Need to run npm install from command prompt in web project folder (eg src/ESFA.DC.LARS.Web), making sure that package.json and package-lock.json are in this folder, to get govuk node module for default styles and scripts

The currently installed version of all node packages can be found in src/ESFA.DC.LARS.Web/package.json

## Typescript debugging in visual studio
This is possible if the following conditions are met:

1.  You enable script debugging in Visual Studio.
2.  Webpack is run in development mode (use "cross-env NODE_ENV=development webpack", this is available through npm script build-dev and should be bound to visual studio build event by default).
3.  You use internet explorer.