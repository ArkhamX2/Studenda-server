import React, { FC } from 'react'
import classes from './LoginInput.module.css'
export enum TextAlign {
  left='left',
  center='center'
}

interface inputProps {
        text?: string;
        align?: TextAlign;
}
const LoginInput: FC<inputProps> =
  ({
    text, 
    align
  }) => {

  return (
       <input placeholder={text} className={classes.LoginInput}  style={{display: 'flex', textAlign: align===TextAlign.left  ? 'left' : 'center'}}></input>
  )
}

export default LoginInput;
