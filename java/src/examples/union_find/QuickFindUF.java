package examples.union_find;

public class QuickFindUF implements UF
{
    private int[] id;

    public QuickFindUF(int[] id)
    {
        this.id = id;
    }

    public boolean connected(int p, int q)
    {
        return root(p) == root(q);
    }

    public void union(int p, int q)
    {
        int i = root(p);
        int j = root(q);

        id[i] = j;
    }

    private int root(int i) {
        while(i != id[i]) {
            i = id[i];
        }

        return i;
    }
}