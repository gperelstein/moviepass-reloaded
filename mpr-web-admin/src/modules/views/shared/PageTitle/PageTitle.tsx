import React, { ReactNode, ReactNodeArray } from 'react';

import { Box, Typography } from '@material-ui/core';
import styles from './styles';

export interface IPageTitle {
  title: string;
  subtitle?: string;
  right?: ReactNode | ReactNodeArray;
  styles?: {};
}

export default function PageTitle({ title, subtitle, right, styles: customStyles = {} }: IPageTitle) {
  return (
    <Box style={{ ...styles.wrapper, ...customStyles }}>
      <Box style={styles.container}>
        <Box style={styles.titleWrapper}>
          <Typography component="h2" style={styles.title} variant="h1">
            {title}
          </Typography>
          {subtitle && <Typography style={styles.subtitle}>{subtitle}</Typography>}
        </Box>
        {right}
      </Box>
    </Box>
  );
}