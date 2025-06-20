import { TestBed } from '@angular/core/testing';

import { SiteInitService } from './site-init-service';

describe('SiteInitService', () => {
  let service: SiteInitService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SiteInitService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
