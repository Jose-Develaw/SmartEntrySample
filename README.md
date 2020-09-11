# SmartEntrySample.CompleteEntry

(This is a work in progress)

## What is SmartEntry.CompleteEntry?

It is a custom entry that includes some properties for making your life easier when building forms whose fields need to be validated.

## How it works?

The magic property is called "<b>BehaveAs</b>". This property is in charge of managing the behaviors attached to the entry so it can validate its content by itself.

Smart, isn't it?

## Other nice stuff

There are other properties that will help you increasing your forms building speed:

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

```
<StackLayout BackgroundColor="WhiteSmoke" Padding="20">
        <Label Text="SmartEntrySample" FontSize="Title" HorizontalTextAlignment="Center" TextDecorations="Underline" HorizontalOptions="CenterAndExpand" TextColor="Gray" Margin="0,0,0,10"/>
        <Label Text="Comportamiento: Cp Requerido: False" TextColor="Gray"/>
        <customcontrols:CompleteEntry BehaveAs="CodPos" Title="Código Postal" ></customcontrols:CompleteEntry>
        <Label Text="Comportamiento: Email; Requerido: True" TextColor="Gray"/>
        <customcontrols:CompleteEntry BehaveAs="Email" IsRequired="True" Title="Email"></customcontrols:CompleteEntry>
        <Label Text="Comportamiento: NIF /Requerido: True / FocusedBorderColor" TextColor="Gray"/>
        <customcontrols:CompleteEntry BehaveAs="NIF" IsRequired="True" Title="NIF (Español)"  FocusedBorderColor="DarkOrange" ></customcontrols:CompleteEntry>
        <Label Text="Comportamiento: NIF /Requerido: True / PlaceHolderColor" TextColor="Gray"/>
        <customcontrols:CompleteEntry BehaveAs="NIF" IsRequired="True" Title="NIF (Español)"  PlaceholderColor="Crimson" ></customcontrols:CompleteEntry>
    </StackLayout>
```
## Let's see the magic in action

(Sample video here)
