import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ConvertToSpacePipe } from '../pipes/convert-to-space.pipe';
import { ProductDetailGuard } from '../guards/product-detail.guard';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    SharedModule,
    RouterModule,
    RouterModule.forChild([
      { path: 'products', component: ProductListComponent }, 
      { path: 'products/:id', canActivate: [ProductDetailGuard], component: ProductDetailComponent }])
    ],
  declarations: [
    ProductListComponent,
    ProductDetailComponent,
    ConvertToSpacePipe
  ]
})
export class ProductModule { }
