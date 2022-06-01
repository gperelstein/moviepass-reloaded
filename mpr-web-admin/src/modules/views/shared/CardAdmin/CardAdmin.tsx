import React from 'react';
import { useStyles } from './styles';
import Typography from '@material-ui/core/Typography';
import { Link, useNavigate } from 'react-router-dom';
import { ROUTES } from '../../../../constants';
interface ICardAdmin {
  title: string;
  bodyFirstLine: string;
  bodySecondLine: string;
  linkDescription: string;
  logo: JSX.Element;
  pathLink: string
}

const CardAdmin = ({ title, bodyFirstLine, bodySecondLine, linkDescription, logo, pathLink }: ICardAdmin) => {
  const styles = useStyles();
  const navigate = useNavigate();
  const redirectToMapping = () => {
    navigate(pathLink);
  };

  return (
    <div className={styles.cardContainer} onClick={() => redirectToMapping()}>
      <div className={styles.titleContainer}>
        <div className={styles.logoContainer}>
            <span className={styles.logo}>        
                {logo}
            </span>
        </div>
        <div>
          <Typography className={styles.title}>{title}</Typography>
        </div>
      </div>
      <div className={styles.bodyContainer}>
        <Typography>{bodyFirstLine}</Typography>
        <Typography>{bodySecondLine}</Typography>
      </div>
      <div className={styles.linkContanier}>
        <Link to={pathLink} color="inherit">
          {linkDescription}
        </Link>
      </div>
    </div>
  );
};

export default CardAdmin;