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

### Check if SQLite database

C#
```
byte[] bytes = new byte[17];
using (IO.FileStream fs = new IO.FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
{
    fs.Read(bytes, 0, 16);
}
string chkStr = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
return chkStr.Contains("SQLite format");
```
