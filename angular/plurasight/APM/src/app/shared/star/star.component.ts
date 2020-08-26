import { Component, OnInit, OnChanges, SimpleChanges, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'pm-star',
  templateUrl: './star.component.html',
  styleUrls: ['./star.component.css']
})
export class StarComponent implements OnChanges {

  starWidth: number;
  @Input() rating: number;

  @Output() notify: EventEmitter<string> = new EventEmitter<string>();

  ngOnChanges(changes: SimpleChanges): void {
    this.starWidth = this.rating * (75 / 5)

    console.log(this.starWidth)
  }

  onClick() {
    this.notify.emit(`The rating ${this.rating} was clicked`);
  }

}
