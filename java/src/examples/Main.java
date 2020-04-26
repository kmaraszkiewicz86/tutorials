package examples;

import examples.std.*;
import examples.union_find.*;

public class Main {

    public static void main(String[] args) {
        new CheckingConnectivity(new BasicFindUF(new int[] { 0, 5 ,4 ,4, 0, 5, 5, 1, 0, 6 })).doWork();

        new CheckingConnectivity(new QuickFindUF(new int[] { 0, 5 ,4 ,4, 0, 5, 5, 1, 0, 6 })).doWork();
    }
}
