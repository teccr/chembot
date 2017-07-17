# Import References
import boto3

# Variables Setup
botName = 'ChemBotAssistant'
botAlias = '$LATEST'

# ---------------------------------------------
# Creating clients
modelClient = boto3.client('lex-models')

# ---------------------------------------------
# Lex Objects Names

# Slots
cmpIdTypeSlotTypName = 'CompoundIdType'
chemicalPropertySlotTypName = 'ChemicalProperty'
compoundIdSlotTypName = 'CompoundId'
attachmentsSlotTypName = 'Attachments'
chemHelpCriteriaSlotTypName = 'ChemBotHelpCriteriaSelection'

slotNames = [ cmpIdTypeSlotTypName, chemicalPropertySlotTypName, compoundIdSlotTypName, attachmentsSlotTypName , chemHelpCriteriaSlotTypName ]

# Intents
chemBotHelpInfoIntent = 'ChemBotHelpInformation'
chemCompInfoIntent = 'ChemicalCompoundInformation'

intentNames = [ chemBotHelpInfoIntent, chemCompInfoIntent ]

# Bot
chemBotName = 'ChemBotAssistant'
chemBotAlias = 'ChemBotProd'

# ---------------------------------------------
# Publish Elements

# Slot Types
print('Publish Slot Types')
for slot in slotNames:
	print('Publishing Slot Type:'+ slot + '...')
	slotInstance = modelClient.get_slot_type(name=slot, version='$LATEST')
	modelClient.create_slot_type_version(name=slot, checksum=slotInstance['checksum'])

# Intents
print('Publish Intents')
for intent in intentNames:
	print('Publishing Intent: '+ intent + '...' )
	intentInstance = modelClient.get_intent(name=intent, version='$LATEST')
	modelClient.create_intent_version(name=intent, checksum=intentInstance['checksum'])

# Bot
print('Publishing Bots')
botInstance = modelClient.get_bot(name=chemBotName, versionOrAlias='$LATEST')
modelClient.create_bot_version(name=chemBotName, checksum=botInstance['checksum'])

# Create Alias
print('Create Alias')
botInstance = modelClient.get_bot(name=chemBotName, versionOrAlias='$LATEST')
try:
	alias = modelClient.get_bot_alias(name = chemBotAlias, botName=chemBotName)
	modelClient.put_bot_alias(name = chemBotAlias, description = 'ChemBot Alias', botVersion = botInstance['version'], botName = chemBotName, checksum=alias['checksum'])
except:
	modelClient.put_bot_alias(name = chemBotAlias, description = 'ChemBot Alias', botVersion = botInstance['version'], botName = chemBotName)
print('ChemBot Published!')