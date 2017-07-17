REM ------------------------------------------
REM Exports the latest definition of ChemBot from its Lex Components.
REM ------------------------------------------

echo Export ChemBot Lex Componets to JSON files

echo Export Slots
aws lex-models get-slot-type --name=ChemBotHelpCriteriaSelection --slot-type-version=$LATEST > LexDefinitions\Slots\ChemBotHelpCriteriaSelection.json
aws lex-models get-slot-type --name=ChemicalProperty --slot-type-version=$LATEST > LexDefinitions\Slots\ChemicalProperty.json
aws lex-models get-slot-type --name=CompoundIdType --slot-type-version=$LATEST > LexDefinitions\Slots\CompoundIdType.json
aws lex-models get-slot-type --name=Attachments --slot-type-version=$LATEST > LexDefinitions\Slots\Attachments.json

echo Export Intents
aws lex-models get-intent --name=ChemBotHelpInformation --intent-version=$LATEST > LexDefinitions\Intents\ChemBotHelpInformation.json
aws lex-models get-intent --name=ChemicalCompoundInformation --intent-version=$LATEST > LexDefinitions\Intents\ChemicalCompoundInformation.json

echo Export Bot
aws lex-models get-bot --name=ChemBotAssistant --version-or-alias=$LATEST > LexDefinitions\Bots\ChemBot.json

echo ChemBot Export completed