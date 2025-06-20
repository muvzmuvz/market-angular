import { TestBed } from '@angular/core/testing';

import { InitStatusService } from './init-status-service';

describe('InitStatusService', () => {
  let service: InitStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InitStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
