import { Component, OnInit } from '@angular/core';
import { IProduct } from './product';
import { ProductService } from '../../services/product.service';
import { from } from 'rxjs';

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
  private _listFilter: string;

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

  filteredProducts: IProduct[];

  constructor (private productService: ProductService) {
    this._listFilter = '';
    this.filteredProducts = this.productService.getProducts();
  }

  ngOnInit(): void {
    this.filteredProducts = this.productService.getProducts();
  }

  toggleImage(): void {
    this.showImage = !this.showImage;
  }

  eventFetcher(message: string): void {
    this.pageTitle = message;
  }

  private performFilter() {

    if (!this._listFilter) {
      this.filteredProducts = this.productService.getProducts();
    }

    let listFilter = this._listFilter.toLocaleLowerCase();

    this.filteredProducts = this.productService.getProducts().filter((product: IProduct) => 
      product.productName.toLocaleLowerCase().indexOf(listFilter) != -1);
  }
}