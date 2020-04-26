package examples.union_find;

public class BasicFindUF implements UF
{
    private int[] id;

    public BasicFindUF(int[] id)
    {
        this.id = id;
    }

    public boolean connected(int p, int q)
    {
        return id[p] == id[q];
    }

    public void union(int p, int q)
    {
        var pid = id[p];
        var qid = id[q];
        for (var index = 0; index < id.length; index++)
        {
            if (pid == id[index])
            {
                id[index] = qid;
            }
        }
    }
}