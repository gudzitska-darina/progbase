using System;
using Terminal.Gui;
using System.Collections.Generic;
public class CreateActivity : Window
{
    protected TextField typeInput;
    protected TextField nameInput;
    protected TextField distInput;
    protected TextField commentInput;
    public bool status;
    public CreateActivity()
    {
        this.Title = "Create new activity";
        Button confirm = new Button(3, 25,"Confirm");
        confirm.Clicked += WindowCreateAct;
        Button cancel = new Button(15, 25, "Cancel");
        cancel.Clicked += WindowCanceled;
        this.Add(confirm, cancel);
        
        Label type = new Label (3, 2, "Type:");
        Label name = new Label (3, 4, "Name:");
        Label dist = new Label (3, 6, "Distance:");
        Label comment = new Label (3, 8, "Comment:");
         
        this.Add(type, name, dist, comment);

        typeInput = new TextField(){X = 15, Y = Pos.Top(type), Width = 10, Text = ""};
        nameInput = new TextField (){X = 15, Y = Pos.Top(name), Width = 10, Text = ""};
        distInput = new TextField (){X = 15, Y = Pos.Top(dist), Width = 10, Text = ""};
        commentInput = new TextField (){X = 15, Y = Pos.Top(comment), Width = 30, Text = ""};

        this.Add(typeInput, nameInput, distInput, commentInput);

    }
    public Activity GetActivity()
    {
        return new Activity()
        {

            type = typeInput.Text.ToString(),
            name = nameInput.Text.ToString(),
            distance = int.Parse(distInput.Text.ToString()),
            comment = commentInput.Text.ToString()
        };
    }

    private void WindowCanceled()
    {
        this.status = false;
        Application.RequestStop();
    } 

    private void WindowCreateAct()
    {
        int parse;
        if(!int.TryParse(distInput.Text.ToString(), out parse))
        {
            MessageBox.ErrorQuery("Incorrect input","Change value of a distance", "OK");
        }
        else
        {
            this.status = true;
            Application.RequestStop();
        }
        
    }

}
