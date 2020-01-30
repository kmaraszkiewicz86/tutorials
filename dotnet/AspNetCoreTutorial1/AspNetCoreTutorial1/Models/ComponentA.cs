namespace AspNetCoreTutorial1.Models
{
    public class ComponentA
    {
	    private readonly IComponent _component;

	    public ComponentA(IComponent component)
	    {
		    _component = component;
	    }
    }
}
