import { TestBed } from '@angular/core/testing';

import { AuthIntercreptor } from './auth-intercreptor';

describe('AuthIntercreptor', () => {
  let service: AuthIntercreptor;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthIntercreptor);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
