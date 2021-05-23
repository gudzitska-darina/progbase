public class ModifyActivyty : CreateActivity
{
    public ModifyActivyty()
    {
        this.Title = "Modificate activity";
    }
    public void SetActivity(Activity act)
    {
        this.typeInput.Text = act.type;
        this.nameInput.Text = act.name;
        this.distInput.Text = act.distance.ToString();
        this.commentInput.Text = act.comment;
    }
}
