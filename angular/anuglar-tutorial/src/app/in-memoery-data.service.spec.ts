import { TestBed } from '@angular/core/testing';

import { InMemoeryDataService } from './in-memoery-data.service';

describe('InMemoeryDataService', () => {
  let service: InMemoeryDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InMemoeryDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
