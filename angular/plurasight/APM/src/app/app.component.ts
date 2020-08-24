import { Component } from '@angular/core';

@Component({
  selector: 'pm-root',
  template: `
    <div>
      <div>
        <h1>{{pageTitle}}</h1>
      <div>
      <pm-product-list></pm-product-list>
    </div>`
})
export class AppComponent {
  pageTitle: String = 'Acme product Managemenet';
}
