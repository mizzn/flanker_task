source("functions.R")

# formからのデータをよみだす
form_data = read.csv("csv/JNNS2024_flanker_answer.csv", header = TRUE, sep = ",")
# View(form_data)

# ID(ファイル名)を取得
ids = form_data[, 7]
# print(id[1])
# print(id[2])

# 分析用dfを作っておく
df = data.frame(
  displayFormat = character(), # 表示形式
  incongruency = character(), # 一致不一致
  RT_ave = numeric() # RT
)
# View(df)

# file_names = c("controll", "2D", "barrier", "Far")
file_name = "controll"
# display_formats = c("VR Object", "2D Object", "VR Object with barrier", "VR Object with distance)
display_format = "VR Object"
id = "AHA2"

# パスつくる
path = paste0("csv/", id, "/")

# csv読み出して確認
response_data = read.csv(paste0(path, id, file_name,".csv"), header = TRUE, sep = ",")
View(response_data)

# 要因ごとに処理
congruent_data = chose_by_incongruency(response_data, "congruent")
filtered_congruent = remove_unnecessary_data(congruent_data)
# View(filtered_data)
congruent_ave = mean(filtered_congruent$RT, na.rm = TRUE)
df = add_row(df, display_format, "congruent", congruent_ave)
View(df)


incongruent_data = chose_by_incongruency(response_data, "incongruent")
filtered_incongruent = remove_unnecessary_data(incongruent_data)
# View(filtered_data)
incongruent_ave = mean(filtered_incongruent$RT, na.rm = TRUE)
df = add_row(df, display_format, "incongruent", incongruent_ave)




# for (id in ids) {
#   print(id)
#   path = paste0("csv/", id, "/")
#   
#   for(file_name in file_names){
#   
#     # csv読んでくる
#     response_data = read.csv(paste0(path, id, file_name,".csv"), header = TRUE, sep = ",")
#     View(f)
#     
#     # 要因ごとにRTの平均と標準偏差
#     congruent_df = response_data[]
#     
#     #RTの平均と標準偏差
#     # controll_RT_ave <- mean(f$RT, na.rm = TRUE)
#     # controll_RT_sd <- sd(f$RT, na.rm = TRUE)
#     
#     # 2SD離れたデータは削除
#     # filtered_f <- f[(controll_RT_ave - 2 * controll_RT_sd) <= f$RT & f$RT <= (controll_RT_ave + 2 * controll_RT_sd),]
#     # View(filtered_f)
#   }
#   
#   # 正解のみ使う
#   # filtered_controll <- filtered_controll[filtered_controll$TF == 1, ]
#   # View(filtered_controll)
# 
#   # 再計算
#   # controll_RT_ave <- mean(filtered_controll$RT, na.rm = TRUE)
#   
#   # 書き込み
#   # add_data <- c(id, controll_RT_ave, controll_error_rate, twoD_RT_ave, twoD_error_rate, barrier_RT_ave, barrier_error_rate, far_RT_ave, far_error_rate)
#   # write.table(t(add_data), file = "results.csv", sep = ",", row.names = FALSE, col.names = FALSE, quote = FALSE, append = TRUE)
# }
