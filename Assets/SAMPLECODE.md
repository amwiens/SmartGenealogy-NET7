# Sample code snippets

### DataGrid

XAML:
```
<DataGrid Grid.Row="1"
          AutoGenerateColumns="False"
          ItemsSource="{Binding People}"
          SelectedItem="{Binding SelectedPerson}">
    <Interaction.Behaviors>
        <EventTriggerBehavior EventName="DoubleTapped">
            <InvokeCommandAction Command="{Binding DoubleTappedCommand}" />
        </EventTriggerBehavior>
    </Interaction.Behaviors>

    <DataGrid.Columns>
        <DataGridTextColumn Header="Given Name" Binding="{Binding FirstName}" />
        <DataGridTextColumn Header="Last Name" Binding="{Binding LastName}" />
    </DataGrid.Columns>
</DataGrid>
```

C#:
```
[RelayCommand]
private void DoubleTapped()
{

}
```