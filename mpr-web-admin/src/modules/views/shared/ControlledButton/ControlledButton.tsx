import React, { memo, useCallback } from 'react';

export interface IControlledInput {
  children: React.ReactNode;
  styleClass?: any;
}

const ControlledButton = ({ children, styleClass = '' }: IControlledInput) => {

  return (
    <span data-testid="controlled-button" tabIndex={-1} className={styleClass}>
      {children}
    </span>
  );
};

export default ControlledButton;