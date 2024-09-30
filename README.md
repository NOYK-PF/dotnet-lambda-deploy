# dotnet_deploy

## ローカル設定
```
dotnet tool install -g Amazon.Lambda.Tools
dotnet tool install -g Amazon.Lambda.TestTool-8.0 

dotnet new -i Amazon.Lambda.Templates
dotnet new lambda --list
dotnet new lambda.EmptyFunction --name MyLambdaFunction

dotnet build
dotnet lambda-test-tool-8.0
```

## リモート設定
samconfig.tomlは配置するが、利用できなかったため、下記環境変数はコンソール上の設定を実施
- Environment
- STACK_NAME
- S3_BUCKET
