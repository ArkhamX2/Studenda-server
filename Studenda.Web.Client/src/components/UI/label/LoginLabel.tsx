import React, { FC } from 'react'
import classes from './LoginLabel.module.css'
export enum TextAlign {
  right='right',
  center='center'
}

interface labelProps {
        text?: string;
        align?: TextAlign;
}
const LoginLabel: FC<labelProps> =
  ({
    text, 
    align
  }) => {

  return (
       <div className={classes.LoginLabel} style={{ alignSelf: align===TextAlign.center  ? 'center' : 'right'}}>{text}</div>
  )
}

export default LoginLabel;
