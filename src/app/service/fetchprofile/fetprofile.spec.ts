import { TestBed } from '@angular/core/testing';

import { Fetprofile } from './fetprofile';

describe('Fetprofile', () => {
  let service: Fetprofile;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Fetprofile);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
