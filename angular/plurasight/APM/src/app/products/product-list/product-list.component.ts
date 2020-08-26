import { Component, OnInit } from '@angular/core';
import { IProduct } from './product'

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

  filteredProducts: IProduct[] = [];

  products: IProduct[] = [
    {
      "productId": 2,
      "productName": "Garden Cart",
      "productCode": "GDN-0023",
      "releaseDate": "March 18, 2019",
      "description": "15 gallon capacity rolling garden cart",
      "price": 32.99,
      "starRating": 4.2,
      "imageUrl": "assets/images/garden_cart.png"
    },
    {
      "productId": 5,
      "productName": "Hammer",
      "productCode": "TBX-0048",
      "releaseDate": "May 21, 2019",
      "description": "Curved claw steel hammer",
      "price": 8.9,
      "starRating": 4.8,
      "imageUrl": "assets/images/hammer.png"
    }
  ];

  constructor () {
    this._listFilter = '';
    this.filteredProducts = this.products;
  }

  ngOnInit(): void {
    this.filteredProducts = this.products;
  }

  toggleImage(): void {
    this.showImage = !this.showImage;
  }

  private performFilter() {

    if (!this._listFilter) {
      this.filteredProducts = this.products;
    }

    let listFilter = this._listFilter.toLocaleLowerCase();

    this.filteredProducts = this.products.filter((product: IProduct) => 
      product.productName.toLocaleLowerCase().indexOf(listFilter) != -1);
  }
}