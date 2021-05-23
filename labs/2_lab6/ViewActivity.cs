using System;
using Terminal.Gui;
using System.Collections.Generic;

public class ViewActivity : Window
{   
    public bool del;
    public bool edited;
    protected Activity activity;
    private TextField idInput;
    private TextField typeInput;
    private TextField nameInput;
    private TextField distInput;
    private TextField commentInput;
    private DateField createdAtInput;
    public ViewActivity()
    {
        this.Title = "Preview of activity";
        Button modify = new Button(5, 2,"Modify");
        modify.Clicked += WindowModifAct;
        Button delete = new Button(15, 2, "Delete");
        delete.Clicked += OnActDelete;
        Button exit = new Button(25, 2, "Back");
        exit.Clicked += WindowBackToMain;
        this.Add(modify, delete, exit);

        Label id = new Label (3, 6, "Id:");
        Label type = new Label (3, 8, "Type:");
        Label name = new Label (3, 10, "Name:");
        Label dist = new Label (3, 12, "Distance:");
        Label comment = new Label (3, 14, "Comment:");
        Label createdAt = new Label (3, 16, "Created at:");
         
        this.Add(id, type, name,comment, dist, createdAt);
    
        idInput = new TextField (){X = 15, Y = Pos.Top(id), Width = 5, Text = "", ReadOnly = true};
        typeInput = new TextField (){X = 15, Y = Pos.Top(type), Width = 10, Text = "", ReadOnly = true};
        nameInput = new TextField (){X = 15, Y = Pos.Top(name), Width = 20, Text = "", ReadOnly = true};
        distInput = new TextField (){X = 15, Y = Pos.Top(dist), Width = 5, Text = "", ReadOnly = true};
        commentInput = new TextField (){X = 15, Y = Pos.Top(comment), Width = 30, Text = "", ReadOnly = true};
        createdAtInput = new DateField (){X = 15, Y = Pos.Top(createdAt), Width = 20, ReadOnly = true};

        this.Add(idInput, typeInput, nameInput, distInput, commentInput, createdAtInput);

    }

    private void OnActDelete()
    {
        int index = MessageBox.Query("Delete", "Are u sure?", "yes", "no");
        if(index == 0)
        {
            this.del = true;
            Application.RequestStop();
        }
    }
    public void SetActivity(Activity act)
    {
        this.activity = act;
        this.idInput.Text = act.id.ToString();
        this.typeInput.Text = act.type;
        this.nameInput.Text = act.name;
        this.distInput.Text = act.distance.ToString();
        this.commentInput.Text = act.comment;
        this.createdAtInput.Text = act.createdAt.ToShortDateString();

    }
    public Activity GetActivity()
    {
        return this.activity;
    }

    private void WindowBackToMain()
    {
        Application.RequestStop();
    }

    private void WindowModifAct()
    {
        ModifyActivyty window = new ModifyActivyty();
        window.SetActivity(this.activity);
        Application.Run(window);
        if(window.status)
        {
            Activity changed = window.GetActivity();
            this.edited = true;
            this.SetActivity(changed);
            
        }
    }
}