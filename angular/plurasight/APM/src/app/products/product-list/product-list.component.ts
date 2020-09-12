import { Component, OnInit } from '@angular/core';
import { IProduct } from './product';
import { ProductService } from '../../services/product.service';
import { Observable, range } from 'rxjs';
import { map, filter } from 'rxjs/operators';

@Component({
  selector: 'pm-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  
  pageTitle: string = 'Product List';
  width: number = 50;
  margin: number = 2;
  showImage: boolean = false;
  errorMessage: String = '';
  private _listFilter: string;

  filteredProducts: IProduct[];

  public get listFilter(): string {
    return this._listFilter;
  }

  public set listFilter(value: string) {
    this._listFilter = value;
    this.performFilter();
  }

  get showImageBtnText(): string {
    return this.showImage ? "Hide Image" : "Show Image";
  }

  

  constructor (private productService: ProductService) {
    this._listFilter = '';
    this.getData();
  }

  ngOnInit(): void {
    this.getData();
  }

  toggleImage(): void {
    this.showImage = !this.showImage;
  }

  eventFetcher(message: string): void {
    this.pageTitle = message;
  }

  rangeItems: Observable<number> = range(0, 10);

  testObservable() {

    {
        let dupa: String = "dupa";
        var dupa2: String = "dupa2";

      this.rangeItems.pipe(
        map(x => x * 200),
        filter(x => x % 2 == 0)
      ).subscribe(x => console.log(x));
    }
  }

  private getData(): void {
    this.productService.getProducts().subscribe({
      next: products => this.filteredProducts = products,
      error: err => this.errorMessage = err
    })
  }

  private performFilter() {

    if (!this._listFilter) {
      this.getData();
    }

    let listFilter = this._listFilter.toLocaleLowerCase();

    this.filteredProducts = this.filteredProducts.filter((product: IProduct) => 
      product.productName.toLocaleLowerCase().indexOf(listFilter) != -1);
  }
}