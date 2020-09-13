namespace duoa {
    @dupa123
    class AddCustomerFunc {
        private _name: string;
        get name(): string {
            return this._name;
        }
        set name(newName: string) {
            if (updateCustomerNameAllowed == true) {
                this._name = newName;
            }
            else {
                alert("Error: Updating Customer name not allowed!");
            }

            this.dupa<number>('a', 'b', 'b')
        }

        dupa<T> (a: T, ...sss: T[]) : T {
            return a;
        }
    }

    function dupa123(target: any) {
        return Object.freeze(target);
    }
}