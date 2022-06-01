import { Pagination } from '@mui/material';
import React, { memo, useCallback } from 'react';

import { useStyles } from './styles';

export interface IPaginationProps {
  page: number;
  count: number;
  styleClass?: string;
  onChange: (page: number) => void;
}

const ControlledPagination = ({ page, count, styleClass = '', onChange }: IPaginationProps) => {
  const classes = useStyles();
  const onPageChange = useCallback((event : React.ChangeEvent<unknown>, newPage : number) => onChange(newPage), [onChange]);
  return <Pagination
            data-testid="pagination"
            className={`${styleClass} ${classes.pagination}`}
            page={page}
            count={count}
            onChange={(event, newPage) => onPageChange(event, newPage)}
        />;
};

export default ControlledPagination;
