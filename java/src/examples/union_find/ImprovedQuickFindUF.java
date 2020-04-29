package examples.union_find;

public class ImprovedQuickFindUF implements UF
{
    private int[] id;
    private int[] sz;

    public ImprovedQuickFindUF(int[] id, int[] sz)
    {
        this.id = id;
        this.sz = sz;
    }

    public boolean connected(int p, int q)
    {
        return root(p) == root(q);
    }

    public void union(int p, int q)
    {
        int i = root(p);
        int j = root(q);

        if (i == j) return;

        if (sz[i] < sz[j]) {
            id[i] = j;
            sz[j] += sz[i];
        } else {
            id[j] = i;
            sz[i] += sz[j];
        }

        id[i] = j;
    }

    private int root(int i) {
        while(i != id[i]) {
            id[i] = id[id[i]];
            i = id[i];
        }

        return i;
    }
}