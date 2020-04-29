package examples.union_find;

import examples.std.*;

public class DynamicConnectivity
{
    private UF uf;

    public DynamicConnectivity(UF uf) {
        this.uf = uf;
    }

    public void doWork()
    {
        while(!StdIn.isEmpty()) {
            var p = StdIn.readInt();
            var q = StdIn.readInt();
            if (!uf.connected(p, q))
            {
                uf.union(p, q);
                StdOut.println(p + " " + q);
            }
            else
            {

            }
        }
    }
}