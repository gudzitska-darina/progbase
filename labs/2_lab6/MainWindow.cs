using System;
using Terminal.Gui;
using System.Collections.Generic;
public class MainWindow : Window
{
    private int pageLength = 4;
    private int numPage = 1;
    private Label page;
    private Label totalPage;
    private ListView allActListView;
    private ActyvityRepository repository;

    public MainWindow()
    {
        MenuBar menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                new MenuItem ("_New", "", OnClickedAdding),
                new MenuItem ("_Quit", "", OnQuit)
            }),
            new MenuBarItem ("_Help", new MenuItem [] {
                new MenuItem ("_About", "", onClickedInfo)
            }),
        });
        this.Add(menu);
        
        Button addNewAct = new Button(1, 3, "Add");
        addNewAct.Clicked += OnClickedAdding;

        totalPage = new Label("?")
        {
            X= Pos.Right(addNewAct) + 3,
            Y = 3,
            Width = 5
        };
        allActListView = new ListView(new List<Activity>())
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };
        allActListView.OpenSelectedItem += OnOpenActivity;
        FrameView frameView = new FrameView("Activity:")
        {
            X = 2,
            Y = 5,
            Width = Dim.Fill() - 4,
            Height = pageLength + 4
        };
        Button prev = new Button(2, 20, "Prev");
        prev.Clicked += OnClickPrev;
        page = new Label("?")
        {
            X = Pos.Right(prev) + 3,
            Y = 20
        };
        Button next = new Button()
        {
            X = Pos.Right(page) + 3 ,
            Y = 20,
            Text = "Next"
        };
        next.Clicked += OnClickNext;
        frameView.Add(allActListView);
        this.Add(addNewAct, totalPage, frameView);
        this.Add(prev, page, next);

    }

    private void OnClickPrev()
    {
        if(numPage == 1)
        {   
            return;
        }
        this.numPage -= 1;
        ShowCurrentPage();
    }
    private void OnClickNext()
    {
        int totalP = repository.GetTotalPages(pageLength);
        if(numPage >= totalP)
        {   
            return;
        }
        this.numPage += 1;
        ShowCurrentPage();

    }
    public void SetRepository(ActyvityRepository repo)
    {
        this.repository  = repo;
        this.ShowCurrentPage();
    }
    
    private void ShowCurrentPage()
    {
        this.page.Text = numPage.ToString();
        this.totalPage.Text = repository.GetTotalPages(pageLength).ToString();
        this.allActListView.SetSource(repository.GetPage(numPage, pageLength));
    }
    private void OnClickedAdding()
    {
        CreateActivity window = new CreateActivity();
        Application.Run(window);

        if(window.status)
        {
            Activity newAct = window.GetActivity();
            long actId = repository.Insert(newAct);
            newAct.id = actId;
            allActListView.SetSource(repository.GetPage(numPage, pageLength));

        
            ViewActivity viewWindow = new ViewActivity();
            viewWindow.SetActivity(repository.GetById(newAct.id));
            Application.Run(viewWindow); 

            checkClick(viewWindow, actId);
        }
    }


    private void OnQuit()
    {
        Application.RequestStop();
    }

    private void OnOpenActivity(ListViewItemEventArgs args)
    {
        ViewActivity viewWindow = new ViewActivity();
        Activity act = (Activity)args.Value;
        viewWindow.SetActivity(act);
        Application.Run(viewWindow);

        checkClick(viewWindow, act.id);
    }
    private void checkClick(ViewActivity viewWindow, long actId)
    {   
        if(viewWindow.del)
        {
           int res = repository.DeleteById(actId);
           if(res == 1)
           {
               int pages = repository.GetTotalPages(pageLength);
               if(numPage > pages)
               {
                   numPage -= 1;
               }
               if(numPage == 0)
               {
                   MessageBox.ErrorQuery("List of activity is empty", "OK");
               }
               allActListView.SetSource(repository.GetPage(numPage, pageLength));
           }
           else
           {
               MessageBox.ErrorQuery("Delete activity","Activity cannot be deleted", "OK");
           }
        }
        if(viewWindow.edited)
        {
            int res = repository.UpdateById(actId, viewWindow.GetActivity());
            if(res == 1)
            {
                allActListView.SetSource(repository.GetPage(numPage, pageLength));
            }
            else
            {
                MessageBox.ErrorQuery("Edit activity","Activity cannot be edited", "OK");
            }
        }
    }
    private void onClickedInfo()
    {
        Dialog win = new Dialog("Information");

        Label info = new Label("The program for managing the essence \"activity\" of the database.\r\nAuthor:Gudzitska Darina)")
        {
            X = 10,
            Y = 5,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        
        Button butt = new Button(50, 10, "OK");
        butt.Clicked += OnQuit;
        win.Add(info, butt);

        Application.Run(win);

    }
}