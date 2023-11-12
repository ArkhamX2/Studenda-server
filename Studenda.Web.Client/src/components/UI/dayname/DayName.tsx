import { FC } from 'react'
import classes from './DayName.module.css'

export enum ButtonVariant {
        outlined='outlined',
        primary='primary'
      }
      interface daynameProps {
              text?: string;
              children?: React.ReactChild | React.ReactNode;
              onClick?: ()=> void;
      }

const DayName: FC<daynameProps> = ({
        text, 
        onClick,
        children

}) => {

    return (
        <div className={classes.buttonBox}>
                <button className={classes.dayButton} onClick={onClick}> Понедельник {text} </button>
                {children}
        </div>
    )
}

export default DayName