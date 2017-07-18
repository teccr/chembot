# ChemBot - A Slack Bot Chemistry Assistant

This bot contains the following features:
* Slack Integration - It was designed to run as a Slack Application.
* PubChem Integration - The bot connects to PubChem Public services to get information about compounds.
* Retrieval of chemical compound - Allows the user to retrieve chemical compound information. 
* Download SDF files - Allow users to get SDF files with chemical properties and chemical structure information.

Add the ChemBot application to your Slack account (Alpha version):

<a href="https://slack.com/oauth/authorize?&client_id=201891926391.213563214386&scope=bot,chat:write:bot,team:read,channels:history"><img alt="Add to Slack" height="40" width="139" src="https://platform.slack-edge.com/img/add_to_slack.png" srcset="https://platform.slack-edge.com/img/add_to_slack.png 1x, https://platform.slack-edge.com/img/add_to_slack@2x.png 2x" /></a>

In a scientific environment, it is important to have references about chemical compounds and its properties. It is common to see third party vendors offering specific solutions and catalogs to consult this information.

ChemBot uses Slack to provide a user-friendly interface to interact with Chemical Compound information. The bot connects to PubChem public services hosted by the National Center for Biotechnology Information ([PubChem](https://www.youtube.com/watch?v=-eNxsNd8sC4) mission). Using ChemBot, developers and scientists can get information and export results as files to be used in third party applications. The out of the box features in Slack give the users a powerful tool to share results quickly, visualize them and follow the progress on the work related to each result. 

For instructions on how to use ChemBot, please visit ChemBot's [Overview](https://google.com). For further details, visit the help section of this document.

# Table of Contents
[Help](#chembot-help)

[Challenges and Limitations](#challenges-and-limitations)

[ChemBot Custom Deployment](#chembot-custom-deployment)

# ChemBot Help

## Basic Interaction
The user interacts with Chembo by asking questions to retrieve chemical Compound data. Example:
```
Get molecular formula for aspirin
```
A search request will contain the following parts:
* ID Type (Identifier Type): Unique identifier to be used in the search. The PubChem REST API requires an explicit specification of the ID Type to use.
* Search Criteria: The value for the identifier. The data will be retrieved based on this field.
* Chemical Property to retrieve: Compound property to retrieve from PubChem database.
* Attachments: Additional information to show in the result for each ChemBot request.

## Identifier Types
The user must work with the available identifier types. They correspond to a PubChem requirement. When the user gets a pop-up asking ID Type, choose the one corresponding to the current search. You can also type the value.
ChemBot supports the following Identifier Types:
* name: Chemical name or any synonym available in the PubChemd system.
* cid: PubChem Compound Identification. Non-zero integer and a unique identifier for chemical structures.
* smiles: Simplified Molecular Input Line Entry System. It is a chemical structure line notation for representing molecules. It uses printable characters and may include wildcards.
* inchi key:  IUPAC International Chemical Identifier, a chemical structure line notation.
* sid: Unique identifier for a depositor-supplied molecule. It is an external registry ID provided by another data source.

## Search Criteria
A value corresponding an identifier type.It will be used as the search criteria to retrieve compound information.
Example:
```
What is the exact mass for glucose?
```
In the previous query, the search criteria are "glucose" and the identifier type is "name". 
Another example:
```
get name for CC1=CC=CC=C1
```
In the previous case, the 'CC1=CC=CC=C1' is the search criteria (it is a SMILES value).

## Chemical Properties
ChemBot allows the slack user to retrieve a good number of chemical properties with different permutations:
* Molecular Formula (MF, mol formula, formula).
* Molecular Weight (MW, mol weight, weight).
* Canonical SMILES (cs, csmiles, smiles).
* Isomeric SMILES (iso smiles, ismiles).
* InChi.
* InChiKey (ikey).
* IUPAC Name (name, iupac, iname).
* XLogP (log p).
* Exact mass (mass).
* Monoisotopic mass (mono iso mas, mono isotopic mass, isotopic mass, iso mass).
* Topological Polar Surface Area (TSPA).
* Hydrogen Bond Donor Count (h bond donor count, h bond donor, bond donor, hydrogen bond donor).
* Hydrogen Bond Acceptor Count (h bond acceptor count, h bond acceptor, bond acceptor).
* Rotatable Bond Count (rotatable bond count, rotatable bond).
* Heavy Atom Count (heavy atom count, heavy atoms).
* Isotope Atom Count (isotope atom count, isotope atoms).
* Atom Stereo Count (atom stereo count, atom stereo).
* Defined Atom Stereocenter Count (defined atom stereo count, defined atom stereo, defined atom ).
* Undefined Atom Stereocenter Count (undefined bond stereo count, undefined bond stereo).
* Bond Stereo Count (bond stereo count, bond stereo).
* Covalently-Bonded Unit Count (covalent unit count, covalent unit, covalent units, covalent bonded units).
* Conformer analytic volume (volume 3d, Conformer analytic volume).
* Steric quadrupole length.
* Steric quadrupole width.
* Steric quadrupole height.
* Features per compound count (features per compound).
The Slack user can access even more information with attachments (see next section).

## Attachments
When the user sends a query to ChemBot backend, the bot asks if it should attach additional information to the results. 
The data will be added as Slack attachments to the message. The options for attachments are:
* None: No Attachment will be added.
* Structure: PNG with the Chemical structure.
* SDF: Link to SDF file containing chemical structure and properties.
Finally, each successful request will always return an attachment: PubChem Reference. This attachment will contain a link to the PubChem web site. The link will show all the available information for the chemical structure.

## Help Intent
There is a help intent in ChemBot. The user can request for help and choose between different topics.
Example: 
```
please help me
```

# Challenges and Limitations

## Chemistry domain knowledge and Amazon Lex
Amazon Lex does a good job a lot of times when dealing with chemistry domain concepts. However, sometimes Amazon Lex may have issues. 
Example:
* Lex cannot recognized the following SMILES as search criteria: C(C1C(C(C(C(O1)O)O)O)O)O (glucose).
* Lex can recognize the following SMILES as search criteria: CC(=O)OC1=CC=CC=C1C(=O)O (Aspirin).
* Lex can recognize the following SMILES as search criteria: CC(=O)OC1=CC=CC=C1C(=O)OC2=CC=CC(=C2)CO[N+](=O)[O-] (Nitroaspirin).
Chemical Structure names, InChi and SMILES can generate issues parsing the user input. After the different tests on ChemBot, moving detection of SMILES, InCHi and names to custom code look like a potential solution. Research on Lambda for Initialization and Validation is pending to deal with the issues mentioned in this section. It is possible the validation code will not be trigger since the SMILES is not picked up by Lex.

## What's next?
ChemBot can receive the following improvements:
* Parsing chemistry domain knowledge
* Adding an AWS cache for images and web requests.
However, the biggest step forward for ChemBot is integration with third party product. Software vendors in the pharmaceutical industry have a number of systems that will benefit from integration with Smart bots. Substance catalogs used to search and buy reactants can be simplified into a chatbot user. Unfortunately, the most important providers don't have public APIs to built on top of it. Showing the power of smart bots may attract interest in building solutions to simplify the life of scientists and students.
Another important piece is the integration with Electronic Notebooks. This kind of product is the fundamental component in pharmaceutical/biotechnological enterprises to record all the R&D process and results across time. One frequent problem is how to get data out of the systems quickly in a secure way and how to share it accordingly. Being able to look for a reagent in a substance catalog, share it with the team in a channel and once it is approved, deploy it to the electronic notebook record associated with the channel.
Chemistry/Biotechnology has many needs regarding system integration and data sharing following security standards. The sector can benefit from smart assistants. 

# ChemBot Custom Deployment

## Requirements to work with the source code:
* Visual Studio 2017 Community Edition / Dot Net Core 1.0/1.1
* AWS Toolkit for Visual Studio 2017.
* AWS CLI (make sure it is configured in the command prompt).
* Python 3.6.1 (make sure it is configured in the command prompt).
* Boto3 Library for Python.

## Steps to deploy your own version of ChemBot:

* Get the latest version of the ChemBot code. Build the code to make sure everything is fine. Optionally run the Unit tests project to verify the functionality.
* Open a command prompt and navigate to the ChemBotFunctions folder.
* From the command prompt, run the "DeployChemBot.cmd" file. Wait for the file to finish.
* Review your AWS Amazon Lex console and make sure the Bot Build process was completed.
* From the command prompt, execute: py PublishChemBot.py
* Wait for the script to finish.
* Finally, open the AWS Lex Console. Click on ChemBotAssistant and click on Publish. Select "ChemBotProd" in the Alias drop down and click Next.

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
