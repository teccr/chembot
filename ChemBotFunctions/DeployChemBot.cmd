REM ------------------------------------------
REM dotnet command needs to be present and the project must have installed the Amazon.Lambda.Tools packages.
REM ------------------------------------------

echo ChemBot - Deployment....
echo Importing Lambda Function

REM Import the Lambda Back End.
dotnet lambda deploy-function ChemBotBackEnd --function-role lambda_basic_execution

REM Create all the Bot infrastructure
echo Create Lex Components

echo Lex Roles
aws iam create-service-linked-role --aws-service-name lex.amazonaws.com

echo Create Slot Types
aws lex-models put-slot-type --name Attachments --cli-input-json file://LexDefinitions/Slots/Attachments.json
aws lex-models put-slot-type --name ChemBotHelpCriteriaSelection --cli-input-json file://LexDefinitions/Slots/ChemBotHelpCriteriaSelection.json
aws lex-models put-slot-type --name ChemicalProperty --cli-input-json file://LexDefinitions/Slots/ChemicalProperty.json
aws lex-models put-slot-type --name CompoundIdType --cli-input-json file://LexDefinitions/Slots/CompoundIdType.json

echo Create Intents
aws lex-models put-intent --name ChemBotHelpInformation --cli-input-json file://LexDefinitions/Intents/ChemBotHelpInformation.json
aws lex-models put-intent --name ChemicalCompoundInformation --cli-input-json file://LexDefinitions/Intents/ChemicalCompoundInformation.json

echo Create Bot
aws lex-models put-bot --name ChemBotAssistant --cli-input-json file://LexDefinitions/Bots/ChemBot.json

echo Chembot Done - Please proceed the Slack integration.