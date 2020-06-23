# Company Requisition and Chat Application

My project is a Windows Forms application that uses the ASP.NET Model, View, Controller (MVC) and MySQL technologies for its backend. The application is intended to be used by companies (more literally, at least two people). In order to utilise it in any meaningful capacity, a login is required, at which point you will be presented with a start page; this differs in appearance, however, based on whether you are a regular user or administrator. All forms have a navigation bar, located at their top.  Explanations about the application’s pages (forms) are given below:

## Sign Up Form
This is for creating a new account and adding a new company or joining under an existing one. Once you have signed up under a given company, you cannot modify this information – in the application, you are bound to that company for your account’s lifespan. It naturally follows that the only way to switch companies would be to delete your account and create a new one. Also, if a username is taken, it cannot be reused, even if signing up under a different company. Sensible data must be entered into each of the fields otherwise the operation will fail.

## Login Form
The form to login to the system. It has two fields, namely a username and password. A checkbox, indicating whether or not you are a system admin (an administrator of this application), exists as well as one indicating whether you are desirous of showing your password. 

## Main Menu Form
This is the form regular users are first met with after logging in. The button entitled “Refresh” refreshes the form, providing the most up-to-date data. Additionally, there is a widget that displays the various responses to item requests that this logged in regular user made to an administrator, each of which can be deleted as you desire.

## Main Menu Admin Form
This is the form administrators are first met with after logging in. There is a widget holding all requests made to you, the logged in administrator; just below are two buttons labelled “Accept” and “Reject”, respectively, each of which affects the selected entry. Additionally, there is a widget that holds information about those who are liable for demotion.

## Request Item Form
This form can only be navigated to by regular users. It allows them to make a request to a given administrator; indeed, the administrator to whom this request is sent to must exist, being present in the provided drop down, otherwise the request will fail. Besides the selection of an admin there are three fields: name, quantity and reason. The quantity can range anywhere between 1 and 999 (it isn’t possible to enter more than 3 characters for this field); in the event a number less than 1 is submitted, the request will still be processed but with a quantity of 1 rather than the number entered by the end-user.

* Name: The name of the desired item.

* Quantity: This item’s desired amount.

* Reason: Your rationale for requesting this item. There is a 200-character limit on this field; toward the bottom of the form a piece of text coloured red shows the current character count (including spaces). 

## Chat Settings Form
A form with a sole function in which new chats can be created. Any user, including chat regular users, can create this chat.

## Chat Form
This form is for sending messages to and forming chats with other members of the same company. A chat has two types of user: chat regular users and chat administrators. There exist widgets for holding the participants of the selected chat, the currently existing chats in the company the logged-in end user signed up under and one showing the messages that have been sent to the selected chat. In order to view a chat’s messages, you must be one of its participants. The exit of a participant from a chat will cause any messages sent to that chat by that user to be deleted. Selected chats and participants are displayed in a light green near the top of the form. A facility for sending messages is provided at the bottom of the form; each message is sent to the selected chat. Furthermore, the buttons pertaining to a given widget are placed just underneath it.

## Update Details Form
This form is for updating your account details. The fields available for modification are your first name, surname, username and password. Any character limits are also specified. Furthermore, your input must comply with a certain standard; in other words, you cannot enter any random value and have it be accepted by the system. There also exist facilities for promoting a system regular user, terminating another person’s account or your own, of which the former two can only be performed by administrators.  
