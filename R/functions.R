chose_by_incongruency = function(data, incongruency) {
  if(incongruency == "congruent"){
    chosen_data = data[data$stimulus == "leftCongruent" | data$stimulus == "rightCongruent", ]
  }else if(incongruency == "incongruent"){
    chosen_data = data[data$stimulus == "leftIncongruent" | data$stimulus == "rightIncongruent", ]
  }else{
    return("ERROR")
  }
  
  return(chosen_data)
}

remove_unnecessary_data = function(data){
  # RTの平均と標準偏差を計算
  ave = mean(data$RT, na.rm = TRUE)
  sd = sd(data$RT, na.rm = TRUE)
  # print(ave)
  # print(sd)
  
  # print((ave - 2 * sd))
  # print((ave + 2 * sd))
  
  # 離れたやつを除く
  filtered_data = data[(ave - 2 * sd) <= data$RT & data$RT <= (ave + 2 * sd),]
  
  # 正解データのみ
  filtered_data = filtered_data[filtered_data$TF == 1,]
  
  return(filtered_data)
}

remove_data = function(data){
  # RTの平均と標準偏差を計算
  ave = mean(data$RT, na.rm = TRUE)
  sd = sd(data$RT, na.rm = TRUE)
  
  # 離れたやつを除く
  filtered_data = data[(ave - 2 * sd) <= data$RT & data$RT <= (ave + 2 * sd),]
  
  return(filtered_data)
}

add_row = function(df, displayformat, incongruency, RT_ave){
  new_row <- data.frame(
    displayformat = displayformat,
    incongruency = incongruency,
    RT_ave = RT_ave
  )
  df <- rbind(df, new_row)
  return(df)
}

add_error_row = function(df, displayformat, incongruency, error_rate){
  new_row <- data.frame(
    displayformat = displayformat,
    incongruency = incongruency,
    error_rate = error_rate
  )
  df <- rbind(df, new_row)
  return(df)
}

add_index_row = function(df, displayformat, index){
  new_row <- data.frame(
    displayformat = displayformat,
    index = index
  )
  df <- rbind(df, new_row)
  return(df)
}

add_row_test = function(df, displayformat, incongruency, RT_ave, condition){
  new_row <- data.frame(
    displayformat = displayformat,
    incongruency = incongruency,
    RT_ave = RT_ave,
    condition = condition
  )
  df <- rbind(df, new_row)
  return(df)
}

get_display_format = function(file_name){
  if(file_name == "controll"){
    display_format = "VR Object"
  }
  else if(file_name == "2D"){
    display_format = "2D Object"
  }else if(file_name == "barrier"){
    display_format = "VR Object with barrier"
  }else if(file_name == "Far"){
    display_format = "VR Object with distance"
  }else{
    display_format = "ERROR"
  }
  return(display_format)
}

get_display_format = function(file_name){
  if(file_name == "controll"){
    display_format = "VR Object"
  }
  else if(file_name == "2D"){
    display_format = "2D Object"
  }else if(file_name == "barrier"){
    display_format = "VR Object with barrier"
  }else if(file_name == "Far"){
    display_format = "VR Object with distance"
  }else{
    display_format = "ERROR"
  }
  return(display_format)
}

from_scene_name_to_file_name = function(scene_name){
  if(scene_name == "FarFlankerTask"){
    file_name = "Far"
  }else if(scene_name == "BarrierFlankerTask"){
    file_name = "barrier"
  }else if(scene_name == "2DFlankerTask"){
    file_name = "2D"
  }else if(scene_name == "FlankerTaskScene"){
    file_name = "controll"
  }else{
    file_name = "ERROR"
  }
  return(file_name)
}