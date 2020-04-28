--USE RateAd;
--SELECT
--	web.users.user_name as name,
--	web.users.email,
--	COUNT(web.playlist.id) as amount
--FROM
--	web.subauthors
--JOIN web.users ON web.subauthors.user_id=web.users.id
--JOIN web.playlist ON web.subauthors.playlist_id=web.playlist.id
--GROUP BY
--	web.users.user_name,web.users.email


--SELECT
--	web.users.user_name as name,
--	web.users.email,
--	web.playlist.title as playlist,
--	web.playlist.description,
--	web.video.title
--FROM web.subauthor
--JOIN web.playlist ON web.playlist.id=web.subauthor.playlist_id
--JOIN web.users ON web.users.id=web.subauthor.user_id
--JOIN web.video ON web.video.playlist_id=web.playlist.id
--ORDER BY
--	web.users.user_name desc;








--SELECT 
--	web.playlist.title as playlist,
--	web.playlist.description,
--	web.videos.title,
--	web.videos.video
--FROM	
--	web.videos
--JOIN web.playlist ON web.playlist.id=web.videos.playlist_id
--ORDER BY
--	web.playlist.id


