# Calendar

## Introduction
In this demo I will show you how to setup the weekly calendar control, how to customize it, provide data to it and make full use of its event handlers.

### Setup

* Install the Calendar Nuget package. To do that, go to:

  Visual Studio -> Tools -> Nuget Package Manager -> Package Manager Console

  In the console, insert the following: `Install-Package WeekCalendar -Version 1.0.0`

* Now that you have installed the Calendar Nuget package; take a look at the CalendarView.Xaml inside CalendarDemo -> Views folder.
  In order to use the calendar you will need to define the namespace in which you will take the control from.
  Therefore, in the upper section where the UserControl namespaces are defined, you should see the following:
  `xmlns:calendar="http://schemas.codevalue.com/controls"`
  To use the calendar, use `<calendar:WeeklyCalendar />`
  
  The result of this should look something like this:
  
  ![Alt text](https://github.com/Muhannado/Calendar/blob/master/Images/DefaultCalendar.PNG)
  
### The Control Structure

The calendar control is a built up of different layers, each layer can be customized independently. The layers are:
1. The Columns headers - The days of the week
2. The Ledger - Time of the day
3. The Time Slots - All the cells in the main layer; for the purpose of time selection and to provide visualization of where the calendar items should be located
4. The Calendar Items Layer - Will draw all the calendar items relevant to this week

## Customization

In this demo I have a provided a design that looks like the screenshot below. I did so by setting up the properties of the calendar, providing Items Source for the calendar items, and data template selectors to customize the different layers, more on that below.

![Alt text](https://github.com/Muhannado/Calendar/blob/master/Images/CustomizedCalendarWithItems.PNG)

### Basic Properties

Below are the basic properties in which you can customize the calendar:

1. CalendarDays - value should be 5 or 7. e.g. `<calendar:WeeklyCalendar CalendarDays="5"/>`
2. MinimumWorkHour / MaximumWorkHour - value [0,23]. will set up the minimum and maximum hour in the Ledger Layer
3. LedgerLayerWidth - Ledger Layer Width
4. ColumnHeaderHeight - Column Header Height
5. CurrentDate - Bind this to the date you want the calendar to show. The date provided will be set calendar's time from the beginning of that date's week
6. ItemsSource - Bind this to the calendar items list you want to display on the Calendar Items layer. More on that below.

### Data Template Selectors

To enable the user of this control to fully customize each layer, I have provided each layer with a Data Template Selector. Each DataTemplateSelector should provide some logic to choose a DataTemplate for that object based on values of some properties.

Listed below all the possible Data Template Selectors in this control.
1. Calendar header items - Property: `WeekViewHeaderItemTemplateSelector`
2. Ledger hour items - Property: `LedgerItemDataTemplateSelector`
3. Time slots - Property: `TimeSlotsTemplateSelector`
4. Calendar Items: `CalendarItemTemplateSelector`

I will fully demonstrate how to set up your own DataTemplateSelector, after that it's all just the same.

#### Data Template Selectors: Setup and demonstration

To set up your own DataTemplateSelector, you need 3 things
1. You own DataTemplateSelector class that extends the class DataTemplateSelector
2. Data Templates for the reffered object
3. Setup in Xaml, to link the DataTemplates with the DataTemplateSelector


* **DataTemplateSelector class**

   Take a look at the DataTemplateSelectors folder in the CalendarDemo project. Let's take the LedgerItemDataTemplateSelector as an example. In there you should see two sections:
   * **DataTemplate Properties:**
   
      You can create as much as you like, depends on the visualization and logic you are choosing for this layer. In this example, I have provided two DataTemplates:
       
           ZeroHour: DataTemplate for displaying round hour. e.g. 7:00
       
           HalfHour: DataTemplate for displaying half hour. e.g. 7:30
       
   * **SelectTemplate Method:**
   
      Provide Logic for choosing the above data templates and return the relevant data template

* **Data Templates**

    Take a look at the CalendarResource.Xaml file under Views folder in the CalendarDemo project. Let's take the ZeroHour and HalfHour DataTemplates as an example.

    I will only explain what these data templates do, and not demonstrate how to write your own Data Template. However, I will provide you with the Data Models for each layer so you will be able to provide the data template specific to that data. For example, in the Ledger Layer the data inside its model is:

   * Time (DateTime) - time in that item
   * IsCurrent (boolean) - if this hour is the current hour in your system.

    **The ZeroHourDataTemplate** will display the current Time in that item, in the following format "HH" (display hour in 24 hour format), with a DataTemplate trigger that will change the foreground of the fond if this time is the current time.
    
    **The HalfHourDataTemplate** on the hand will display an empty border
    
 * **XAML Setup**
 
     To set up the Data Template Selector to be used in the calendar, you will need to provide the namespace that the DataTemplateSelectors are located under and set up your DataTemplateSelector in the Resources of your control. 
     
     Take a look at the CalendarView.Xaml file under Views folder in the CalendarDemo project, you should see in the first section under <UserControl>, the following namespace `xmlns:dataTemplateSelectors="clr-namespace:CalendarDemo.DataTemplateSelectors"`.
     
     Then under <UserControl.Resources> define your DataTemplateSelector as follows: 
     
     `<dataTemplateSelectors:LedgerItemDataTemplateSelector x:Key="LedgerItemDataTemplateSelector"
                                                              ZeroHour="{StaticResource ZeroHourDataTemplate}"
                                                              HalfHour="{StaticResource HalfHourDataTemplate}"/>`
                                                              
    * As you can see I have used the namespace I have defined, provided it with a key, so later I will be able to provide it to the calendar by that key. Lastly, I have provided it with the DataTemplates I have written under CalendarResource.Xaml.
    * **NOTE:** To be able to use whatever you are writing under CalendarResource.Xaml, you will need to setup this file as a ResourceDictionary. To do that, check out what I did under App.Xaml, where I have defined it and therefore I am able to reference anything in it.
    
    * Lastly, provide the the DataTemplateSelector to the Calendar control:
    
        `<calendar:WeeklyCalendar LedgerItemDataTemplateSelector="{StaticResource LedgerItemDataTemplateSelector}" />`
        
 ##### Data Templates Models
 
 1. The Columns Headers: DateTime in that day
 2. Ledger Item
    * Time (DateTime) - time in that item
    * IsCurrent (boolean) - if this hour is the current hour in your system. Note: if the CurrentDate is in the current system week, this value has effect.
 3. Calendar Item - ICalendarItem interface
    
    *Note: Take a look at the implementation of this interface in the CalendarItemViewModel.cs under the ViewModels folder*
    * DisplayName (string) - Title for that item
    * StartTime (DateTime) - The time which this calendar item starts on
    * EndTime (DateTime?) -  The time which this calendar item ends on (nullable DateTime)
    * IsFullDay (boolean) - FullDay Report, for the purpose of drawing, to make it span the whole day
    * IsEditable (bool) - If this is false, it means that the item's time cannot be changed
    * IsDraggable (bool) - The option to drag the calendar item accros the grid
    
 4. Time Slot
    * SlotFromTime (DateTime) - (e.g. 7:00)
    * SlotUntilTime (DateTime) - (e.g. 7:30)
    * Coordinates (Point) - row/column coordinates in the grid
    * IsSelected (boolean) - if you select this time slot by clicking on it
    
    In the screenshot below you can see how I provided data templates to make use of the above properties, by displaying the SlotFromTime - SlotUntilTime and changing the background color of the cell when it is selected. In addition you can see that when the time in that cell is above 18 oclock or if it is friday or saturday, the colors switches to gray.
    
    ![Alt text](https://github.com/Muhannado/Calendar/blob/master/Images/CustomizedCalendar.PNG)

 
### More Styling

You can provide style for the line that represents the current hour, take a look at in the WeeklyCalendar properties in the demo:

`CurrentHourLineBorderStyle="{StaticResource CurrentHourLineBorderStyle}"`

You can also provide style for the Pop Up button that appears when you select a time slot, take a look at:

`PopUpButtonStyle="{StaticResource PopUpButtonStyle}"`

CurrentHourLineBorderStyle and PopUpButtonStyle are located under CalendarResource.xaml

## Provide Data

To provide data to the control you should bind your calendar items collection to the ItemsSource property.

For this section you will need the following:
1. A class that implements the ICalendarItem interface. 
   * Take a look at the CalendarItemViewModel.cs class under ViewModels folder
2. A collection of ICalendarItem objects. 
   * Take a look at the ObservableCollection in the CalendarViewModel.cs under the ViewModels folder
   
To add more items to the calendar, add them to the list. To remove any item from the control, remove it from the list.

## Event Handlers

In the calendar control, there are two events you can subscribe to:
* OpenCalendarItemEditor 
  If you click on a calendar item, or if you select multiple time slots cells and click on the "add" button; it will be caught by this event.
* UpdateCalendarItem
  If you drag any calendar item to another time, or change the calendar item's end time by dragging its bottom edge; it will be caught by this event
  
  In the CalendarView.Xaml I have provided these events with the following handlers
  
  `cal:Message.Attach="[Event OpenCalendarItemEditor] = [Action EditCalendarItemAsync($eventArgs)]; [Event UpdateCalendarItem] = [Action UpdateCalendarItemAsync($eventArgs)]"`
  
  **Event Handlers Implementation:** in the CalendarViewModel.cs under ViewModels folder
  
  * UpdateCalendarItem 
    The EventArgs of this event are `UpdateCalendarItemEventArgs`, which includes in it the `UpdatedCalendarItem` ICalendarItem. This calendar item holds the updated dates
   
  * EditCalendarItem
    The EventArgs of this event are `CalendarItemEventArgs`, which includes in it the following:
    * IsNewItem (bool) - if you select time slots and click add, then this is true.
    * StartTime (DateTime) - if you select time slots and click add, then this time will be set
    * EndTime (DateTime) - if you select time slots and click add, then this time will be set
    * CalendarItem (ICalendarItem) - if you click on any existing calendar item in the calendar, this will be set, and all the other properties will not be relevant
    
    
