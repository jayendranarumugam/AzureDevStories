# Serverless Prediction of a Product (#AzureDevStories)


## Architecture Diagram

![](./Images/AzureDevStories.jpg)



## Components Used

- Yammer
- LUIS
- Logic Apps
- AzureDevOps WorkItems
- Cosmos DB
- Azure Functions
- Signal R

### Yammer 

Users provide their feedback about the product. It could be many, for the demo purpose I just choose 2 topics *(Bug, Feature)*

![](./Images/Yammer_Post.jpg)

### LUIS

Creating Intents for Bugs and Feedbacks in the LUIS.

![](./Images/LUIS.jpg)

### Logic Apps

Predicting the Intents i.e, Bug or Feedback based on the Yammer Post by the user and take necessary actions

![](./Images/LogicApp.jpg)

### AzureDevOps WorkItems

Create Bug/Feature if the top intent of the Post matched with LUIS 

![](./Images/AzureDevOps_WorkItems.JPG)



### Cosmos DB

Insert the document in Cosmos DB if the top intent of the Post is *None*

![](./Images/CosmosDB.jpg)


### AzureFunction

Azure Function Integrated with Cosmos Change feed and provide live Updates in the static Web Page with the help of Signal R

![](./Images/AzFunction.JPG)

Static WebPage with Live CosmosDB Feed for *None* intent which is used to retrain the *LUIS* 

![](./Images/AzFunction_CosmosLiveFeed.JPG)


### Signal R

Serverless Signal R used to autorefresh the WebPage for the Live updates of the *None* intent

![](./Images/SignalR.JPG)



