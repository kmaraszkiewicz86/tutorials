import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'pm-star',
  templateUrl: './star.component.html',
  styleUrls: ['./star.component.css']
})
export class StarComponent implements OnChanges {

  starWidth: number = 4;
  rating: number;

  ngOnChanges(changes: SimpleChanges): void {
    this.starWidth = this.rating * 75 / 5
  }

}
