import React, { FC } from 'react'
import classes from './Selector.module.css'


interface labelProps {
        text?: string;
        name: string;
        id: string;
}
const Selector: FC<labelProps> = ({
        text,
        name,
        id
}) => {

  return (
        <select className={classes.GroupSelector}name={name} id={id}>
                <option>--{text}--</option>
              </select>  
  )
}

export default Selector;