package examples;

import examples.std.*;
import examples.union_find.*;

public class Main {

    public static void main(String[] args) {
        var id = new int[] { 0, 5 ,4 ,4, 0, 5, 5, 1, 0, 6 };
        var sz = new int[] { 5, 1 ,0 ,0, 2, 4, 1, 0, 0, 0 };

//        new CheckingConnectivity(new BasicFindUF(id)).doWork();
//
//        new CheckingConnectivity(new QuickFindUF(id)).doWork();

        new CheckingConnectivity(new ImprovedQuickFindUF(id, sz)).doWork();
    }
}
