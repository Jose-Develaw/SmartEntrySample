# SmartEntrySample.CompleteEntry

(This work is currently in development)

## What is SmartEntry.CompleteEntry?

It is a custom entry that includes some properties for making your life easier when building forms composed of fields that need to be validated.

## How it works?

The magic property is called "<b>BehaveAs</b>". This property is in charge of managing the behaviors attached to each entry so it can automatically validate its content.

Smart, isn't it?

## Other nice stuff

There are other properties that will help you increase your form building speed:

Property | Type | Explanation | Default |
-------- | ---- | ----------- | --------|
IsRequired| boolean |If true, it will show the "RequiredLabel" with the message you chose.| False |
RequiredText| string |This is where you choose the message showed by the "RequiredLabel"| Campo Obligatorio |
FocusedBorderColor| color ||  Color.Gray |
PlaceHolderColor| color ||  Color.Gray |

## Current behavior list (validations)

* Spanish Postal Code
* Spanish NIF
* Spanish CIF
* Email

You don't have to worry about the names, it uses an enum, so you will know!

## Code sample

Managing required fields with 'IsRequired' and 'RequiredText'
```

 <customcontrols:CompleteEntry BehaveAs="Email" IsRequired="True" Title="Email" RequiredText="*This field is required"/>
 
 ```
 
 Managing colors with 'BorderColor', 'FocusedBorderColor', 'PlaceholderColor' and 'TextColor'
 
 ```
  <customcontrols:CompleteEntry BehaveAs="ES_PosCode" Title="Código Postal" BorderColor="Green" FocusedBorderColor="Purple" PlaceholderColor="DarkOrange" TextColor="Purple"/>
```
## Let's see the magic in action
![me](https://github.com/Jose-Develaw/SmartEntrySample/blob/master/completeentrysample.gif)




