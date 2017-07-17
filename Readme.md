# ChemBot - A Slack Bot Chemistry Assistant

This bot contains the following features:
* Slack Integration - It was designed to run as a Slack Application.
* PubChem Integration - The bot connects to PubChem Public services to get information about compounds.
* Retrieval of chemical compound - Allows the user to retrieve chemical compound information. 
* Download SDF files - Allow users to get SDF files with chemical properties and chemical structure information.

Add the ChemBot application to your Slack account (Alpha version):

<a href="https://slack.com/oauth/authorize?&client_id=201891926391.213563214386&scope=bot,chat:write:bot,team:read,channels:history"><img alt="Add to Slack" height="40" width="139" src="https://platform.slack-edge.com/img/add_to_slack.png" srcset="https://platform.slack-edge.com/img/add_to_slack.png 1x, https://platform.slack-edge.com/img/add_to_slack@2x.png 2x" /></a>

In a scientific environment, it is important to have references about chemical compounds and its properties. It is common to see third party vendors offering specific solutions and catalogs to consult this information.

ChemBot uses Slack to provide a user-friendly interface to interact with Chemical Compound information. The bot connects to PubChem public services hosted by the National Center for Biotechnology Information ([PubChem](https://www.youtube.com/watch?v=-eNxsNd8sC4) mission). Using ChemBot, developers and scientists can get information and export results as files to be used in third party applications. The out of the box features in Slack give the users an powerful tool to share results quickly, visualize them and follow the progress on the work related to each result. 

For instructions on how to use ChemBot, please visit ChemBot's [tutorial](https://google.com).

## Requirements to work with the source code:
* Visual Studio 2017 Community Edition / Dot Net Core 1.0/1.1
* AWS Toolkit for Visual Studio 2017.
* AWS CLI (make sure it is configured in the command prompt).
* Python 3.6.1 (make sure it is configured in the command prompt).
* Boto3 Library for Python.

## Steps to deploy your own version of ChemBot:

Get the latest version of the ChemBot code. Build the code to make sure everything is fine. Optionally run the Unit tests project to verify the functionality.

Open a command prompt and navigate to the ChemBotFunctions folder.

From the command prompt, run the "DeployChemBot.cmd" file. Wait for the file to finish.

Review your AWS Amazon Lex console and make sure the Bot Build process was completed.

From the command prompt, execute: py PublishChemBot.py

Wait for the script to finish.

Finally, open the AWS Lex Console. Click on ChemBotAssistant and click on Publish. Select "ChemBotProd" in the Alias drop down and click Next.

## Slack Integration:

ChemBot follows a straight forward integration process with Slack. If you are not familiar, you can check Amazon's example [here](http://docs.aws.amazon.com/lex/latest/dg/slack-bot-association.html).
The following procedure is pretty similar with one modification in the permissions.

### On Slack API Console
* In the Slack API console, Create a new Slack Application.
* Navigate to "Bot Users", add a new bot user and turn on the following option: "Always Show My Bot as Online". Save Changes.
* Navigate to Interactive Messages. Click on "Enable interactive Messages" and specify the following value for "Request URL" : https://slack.com
* Click on "Save Changes".
* Navigate to "Basic Information", make note of the following credentials: Client ID, Client Secret, Verification Token.

### On AWS Lex Console
* Sign in to AWS Management Console, navigate to Amazon Lex.
* Open the Bots menu and choose "ChemBotAssistant".
* Click on "Channels" tab and select the "Slack" option.
* On the Slack page, enter a name for the Chatbot. Example: "ChemBotSlack".
* Choose "aws/lex" from KMS key drop down.
* Pick "ChemBotProd" in the Alias drop down field.
* Enter Client Id, Client secret and Verification token (the data previously recorded from Slack API console).
* Enter "Success Page URL" value: https://slack.com
* Click on "Activate".
* The AWS console will generate a Postback URL and OAuth URL. Record both values.

### On Slack API Console
* Navigate to the Slack API console and select your ChemBot Application.
* Select "OAuth & Permissions"
* In "Redirect URLs", add a new Redirect URL and enter the "OAuth URL" from the AWS Console.
* In Permission scopes, add the following permissions: 
	* chat:write:bot
	* team:read
	* channels:history
* Save changes
* Navigate to "Interactive Messages", set the "Request URL" field to the value of "Postback URL" from the AWS Console.
* Save changes.
* Navigate to "Event Subscriptions". Turn on the events.
* Set Request URL to "Postback URL" from the Amazon Lex Console.
* Subscribe to the following bot events:
	* message.im
	* message.channels
* Save changes.
* Finally, add the Bot to a channel.
