import { TestBed } from '@angular/core/testing';

import { FetchProfile } from './fetprofile';

describe('Fetprofile', () => {
  let service: FetchProfile ;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FetchProfile );
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
