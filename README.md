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
RequiredText| string |This is where you choose the message showed by the "RequiredLabel"| Required field |
FocusedBorderColor| color ||  Color.Gray |
PlaceHolderColor| color ||  Color.Gray |

## Current behavior list (validations)

Name | Validation | Default Error Message |
-------- | ---- | ----------- |
Email | Email | Email no válido | 
ES_PosCode | Spanish Postal Code | Código Postal no válido|
ES_NIF | Spanish NIF | NIF no válido | 
ES_CIF | Spanish CIF | CIF no válido | 


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
![me](https://github.com/Jose-Develaw/SmartEntrySample/blob/master/SampleGif.gif)


## Custom SmartBehaviors

I already can hear you complaining... <em>"This looks like a pretty closed system to me... What if I want to add my own validator?"</em>

No worries. Just follow these two simple steps:

1) Create your custom validator by inheriting from the awesome SmartBehavior class. You will have to add this constructor and override the IsTextValid (there is where you add your validation method)

```
public class MyCustomValidator : SmartBehavior
    {
        public MyCustomValidator(string ErrorText) : base(ErrorText) { }

        public override bool IsTextValid(string text)
        {
            throw new NotImplementedException();
        }
    }
   ```
   
   Wow, that was pretty easy... Now go for the second step.

2) Create a new SmartEntry.CompleteEntry and add you shinny custom validator to it

```
<customcontrols:CompleteEntry x:Name="MyCustomControl" Title="MyCustomControl"/>
```
```
MyCustomControl.Behaviors.Add(new MyCustomValidator("My custom error message"));

```

That's it.

BTW, if you create some incredible validators and you want them to be added to the main package, just let me know!!


