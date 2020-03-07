import { CanAccessDirective } from './can-access.directive';
import { TestBed } from '@angular/core/testing';

describe('CanAccessDirective', () => {
  it('should create an instance', () => {
    const directive: CanAccessDirective = TestBed.get(CanAccessDirective);
    expect(directive).toBeTruthy();
  });
});
